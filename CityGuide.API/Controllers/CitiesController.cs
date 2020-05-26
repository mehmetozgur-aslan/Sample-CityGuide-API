﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityGuide.API.Data;
using CityGuide.API.Dtos;
using CityGuide.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;

        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            this._appRepository = appRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetCities()

        {
            //var cities = _appRepository.GetCities().Select(c => new CityForListDto
            //{
            //    Description = c.Description,
            //    Name = c.Name,
            //    Id = c.Id,
            //    PhotoUrl = c.Photos.FirstOrDefault(p => p.IsMain == true).Url
            //}).ToList();

            var cities = _appRepository.GetCities();
            List<CityForListDto> citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);          

            return Ok(citiesToReturn);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]City city)
        {
            _appRepository.Add<City>(city);
            _appRepository.SaveAll();

            return Ok(city);
        }

        [HttpGet]
        [Route("detail")]
        public ActionResult GetCityById(int id)
        {
            var city = _appRepository.GetCityById(id);
            CityForDetailDto cityToReturn = _mapper.Map<CityForDetailDto>(city);
            
            return Ok(cityToReturn);
        }

        [HttpGet]
        [Route("photos")]
        public ActionResult GetPhotosByCity(int cityId)
        {
            var photos = _appRepository.GetPhotosByCity(cityId);

            return Ok(photos);
        }
    }
}