using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.InterfaceService;
using Microsoft.EntityFrameworkCore;
using Models.Context;
using Models.DTOs;
using Models.DTOs.Document;
using Models.DTOs.Fight;
using Models.Entity.Fight;
using DocumentEntity = Models.Entity.Document.Document;
namespace Core.ManagerSerice
{
    public class FlightService:IFlightService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDocumentService _documentService;
        public FlightService(AppDbContext appDbContext, IDocumentService documentService)
        {
            _appDbContext = appDbContext;
            _documentService = documentService;
        }
        private async Task<FlightResponse> ConvertToFlightResponse(string flightNo)
        {
            var result = await  _appDbContext.Flight
                                             .Where(f => f.Active == 1 && f.Del_flag == 0 && f.FlightNo == flightNo)
                                             .Select(f=> new FlightResponse
                                             {
                                                 FlightNo = f.FlightNo,
                                                 Route = $"{f.DepartureStation.Code} - {f.ArrivalStation.Code}",
                                                 DepartureDate = f.DepartureDate,
                                                 Documents = f.Documents.Select(d => new DocumentResponse
                                                 {
                                                     DocumentName = d.DocumentName,
                                                     DocumentType = d.DocumentType,
                                                     DocumentVersion = d.DocumentVersion,
                                                     Path = d.Path,
                                                 }).ToList()
                                             }).FirstOrDefaultAsync();

            if (result != null)
            {
                return result;
            }

            return null;
        }
        public async Task<IEnumerable<FlightResponse>> ConvertFlightsToResponses(IEnumerable<Flight> flights)
        {
            return flights.Select(f => new FlightResponse
            {
                FlightNo = f.FlightNo,
                Route = $"{f.DepartureStation.Code} - {f.ArrivalStation.Code}",
                DepartureDate = f.DepartureDate,
                Documents = f.Documents.Select(d => new DocumentResponse
                {
                    DocumentName = d.DocumentName,
                    DocumentType = d.DocumentType,
                    DocumentVersion = d.DocumentVersion,
                    Path = d.Path,
                }).ToList()
            });
        }
        public async Task<Response<IEnumerable<FlightResponse>>> GetAllFlightAsync()
        {
            var response = new Response<IEnumerable<FlightResponse>>();
            try
            {
                var flights = await _appDbContext.Flight.Where(f => f.Active == 1 && f.Del_flag == 0)                                                        
                                                        .Include(f => f.DepartureStation)
                                                        .Include(f => f.ArrivalStation)
                                                        .Include(f => f.Documents.Where(d=>d.Active==1))                                                           
                                                        .ToListAsync();
                                                        
                //var flightResponses = flights.Select(ConvertToFlightResponse).ToList();
                if (flights != null && flights.Any())
                {
                    response.Data =  await ConvertFlightsToResponses(flights);
                    response.StatusCode = 200;
                    response.Message = "Flights retrieved successfully";
                    response.Succes = true;
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "No Flights found";
                    response.Succes = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
                response.StatusCode = 500;
                response.Succes = false;
            }
            return response;

        }
        private async Task<Flight> CreateNewFlightAsync(FlightRequest flightRequest)
        {
            var newFlight = new Flight()
            {
                FlightNo = flightRequest.FlightNo,
                DepartureStationID = flightRequest.DepartureStationID,
                ArrivalStationID = flightRequest.ArrivalStationID,
                DepartureDate = flightRequest.DepartureDate,
                CreatedBy = 5,
                UpdatedBy = 5,
            };
            
            _appDbContext.Flight.Add(newFlight);
            await _appDbContext.SaveChangesAsync();
            if (flightRequest.Document != null)
            {
                var doc = new DocumentRequest
                {
                    DocumentDetail = flightRequest.Document,
                    DocumentTypeId = 1,
                    FlightId = newFlight.Id,
                };
                _documentService.IsDocumentSavedAsync(doc);
               
            }
            return newFlight;
        }
        public async Task<Response<FlightResponse>> AddFlightAsync(FlightRequest flight)
        {
            var response = new Response<FlightResponse>();
            try
            {
                bool isFlightExist = await _appDbContext.Flight.AnyAsync(f => f.FlightNo == flight.FlightNo);

                if (isFlightExist)
                {
                    response.Message = "Flight with the same FlightNo already exists";
                    response.StatusCode = 401;
                    response.Succes = false;
                    return response;
                }
                var newFlight = await CreateNewFlightAsync(flight);
                var data = await ConvertToFlightResponse(newFlight.FlightNo);
                if (data != null)
                {
                    response.Data = data;
                    response.StatusCode = 201;
                    response.Message = "Added new flight successfully";
                    response.Succes = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
                response.StatusCode = 500; 
                response.Succes = false;
            }
            return response;
        }
        public async Task<Response<IEnumerable<FlightResponse>>> GetFlightsByDepartureAsync(int departureId)
        {
            var response = new Response<IEnumerable<FlightResponse>>();
            try
            {
                var flightsByDeparture = await _appDbContext.Flight.Where(
                                                            f => f.Active == 1 && 
                                                            f.Del_flag == 0 && 
                                                            f.DepartureStationID == departureId )
                                                        .Include(f => f.DepartureStation)
                                                        .Include(f => f.ArrivalStation)
                                                        .Include(f => f.Documents.Where(d => d.Active == 1))
                                                        .ToListAsync();
                var result = await ConvertFlightsToResponses(flightsByDeparture);
                if (result != null && result.Any())
                {                    
                    response.Data = result;
                    response.StatusCode = 200;
                    response.Message = $"Retrieve flights departing from \"{flightsByDeparture.First().DepartureStation.Name}\" successfully";
                    response.Succes = true;                    
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "No Flights found";
                    response.Succes = false;
                }
            }
            catch(Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
                response.StatusCode = 500;
                response.Succes = false;
            }
            return response;
        }

        public async Task<Response<IEnumerable<FlightResponse>>> GetFlightsByArrivalAsync(int arrivalId)
        {
            var response = new Response<IEnumerable<FlightResponse>>();
            try
            {
                var flightsByDeparture = await _appDbContext.Flight.Where(
                                                            f => f.Active == 1 &&
                                                            f.Del_flag == 0 &&
                                                            f.ArrivalStationID == arrivalId)
                                                        .Include(f => f.DepartureStation)
                                                        .Include(f => f.ArrivalStation)
                                                        .Include(f => f.Documents.Where(d => d.Active == 1))
                                                        .ToListAsync();
                var result = await ConvertFlightsToResponses(flightsByDeparture);
                if (result != null && result.Any())
                {
                    response.Data = result;
                    response.StatusCode = 200;
                    response.Message = $"Retrieve flights arriving at \"{flightsByDeparture.First().ArrivalStation.Name}\" airport successfully";
                    response.Succes = true;
                }
                else
                {
                    response.StatusCode = 404;
                    response.Message = "No Flights found";
                    response.Succes = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
                response.StatusCode = 500;
                response.Succes = false;
            }
            return response;
        }
    }
}
