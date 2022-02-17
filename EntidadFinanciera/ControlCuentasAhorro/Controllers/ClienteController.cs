using ControlCuentasAhorro.Extensions;
using ControlCuentasAhorro.Models;
using ControlCuentasAhorro.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Controllers
{
    public class ClienteController : Controller
    {
        readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
                TempData.Remove("Message");
            }

            var clientes = _clienteService.ObtenerClientes().ToClienteVM();
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new ClienteVM());
        }

        [HttpPost]
        public IActionResult Crear(ClienteVM model)
        {
            if (ModelState.IsValid)
            {
                var resultado = _clienteService.CrearCliente(model.ToCliente());

                if (resultado == 1)
                {
                    TempData["Message"] = "Guardado con éxito";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Error al guardar";
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Editar(int idcliente)
        {
            return View(_clienteService.ObtenerCliente(idcliente).ToClienteVM());
        }

        [HttpPost]
        public IActionResult Editar(ClienteVM model)
        {
            if (ModelState.IsValid)
            {
                var resultado = _clienteService.EditarCliente(model.ToCliente());

                if (resultado == 1)
                {
                    TempData["Message"] = "Guardado con éxito";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Error al guardar";
                }
            }
            return View(model);
        }
    }
}
