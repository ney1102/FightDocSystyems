using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.InterfaceService;
using Models.DTOs.Fight;
using Models.DTOs;
using Models.Entity.Fight;
using Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.ManagerSerice
{
    public class AirportService:IAirportService
    {
        private readonly AppDbContext _appDbContext;
        public AirportService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Response<IEnumerable<Airport>>> GetAllAirPortAsync()
        {
            var response = new Response<IEnumerable<Airport>>();

            try
            {
                var airports = await _appDbContext.Airport.ToListAsync();

                if (airports != null && airports.Any())
                {
                    response.Data = airports;
                    response.StatusCode = 200;
                    response.Message = "Airports retrieved successfully";
                    response.Succes = true;
                }
                else
                {
                    response.Message = "No airports found";
                    response.StatusCode = 404;
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
