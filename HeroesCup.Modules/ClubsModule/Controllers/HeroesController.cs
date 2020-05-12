using ClubsModule.Services.Contracts;
using HeroesCup.Data.Models;
using Piranha.Manager.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubsModule.Controllers
{
    public class HeroesController : ManagerController
    {
        private readonly IHeroesService heroesService;

        public HeroesController(IHeroesService heroesService)
        {
            this.heroesService = heroesService;
        }


    }
}
