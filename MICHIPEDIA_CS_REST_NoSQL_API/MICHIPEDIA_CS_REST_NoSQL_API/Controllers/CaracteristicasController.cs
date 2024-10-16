﻿using MICHIPEDIA_CS_REST_NoSQL_API.Exceptions;
using MICHIPEDIA_CS_REST_NoSQL_API.Models;
using MICHIPEDIA_CS_REST_NoSQL_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MICHIPEDIA_CS_REST_NoSQL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaracteristicasController(CaracteristicaService caracteristicaService) : Controller
    {
        private readonly CaracteristicaService _caracteristicaService = caracteristicaService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasCaracteristicas = await _caracteristicaService
                .GetAllAsync();

            return Ok(lasCaracteristicas);
        }

        [HttpGet("{caracteristica_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string caracteristica_id)
        {
            try
            {
                var unaCaracteristica = await _caracteristicaService
                    .GetByIdAsync(caracteristica_id);

                return Ok(unaCaracteristica);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Caracteristica unaCaracteristica)
        {
            try
            {
                var caracteristicaCreada = await _caracteristicaService
                    .CreateAsync(unaCaracteristica);

                return Ok(caracteristicaCreada);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error en la validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error en la operación de la DB {error.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Caracteristica unaCaracteristica)
        {
            try
            {
                var caracteristicaActualizada = await _caracteristicaService
                    .UpdateAsync(unaCaracteristica);

                return Ok(unaCaracteristica);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAsync(string caracteristica_id)
        {
            try
            {
                var caracteristicaEliminada = await _caracteristicaService
                    .RemoveAsync(caracteristica_id);

                return Ok(caracteristicaEliminada);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }



    }
}
