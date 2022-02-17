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
    public class CuentaAhorroController : Controller
    {
        readonly ICuentaAhorroService _cuentaAhorroService;
        readonly IClienteService _clienteService;

        public CuentaAhorroController(ICuentaAhorroService cuentaAhorroService, IClienteService clienteService)
        {
            _cuentaAhorroService = cuentaAhorroService;
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Gestionar(int idcliente)
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
                TempData.Remove("Message");
            }

            var cliente = _clienteService.ObtenerCliente(idcliente);
            ViewBag.NombreCliente = $"{cliente.Nombre ?? ""} {cliente.ApellidoPaterno ?? ""} {cliente.ApellidoMaterno ?? ""}".Trim();
            ViewBag.IdCliente = idcliente;

            var cuentas = _cuentaAhorroService.ObtenerCuentaAhorros(idcliente).ToCuentaAhorroVM();
            return View(cuentas);
        }

        [HttpGet]
        public IActionResult Crear(int idcliente)
        {
            var cliente = _clienteService.ObtenerCliente(idcliente).ToClienteVM();

            return View(new CuentaAhorroVM() { Cliente = cliente });
        }

        [HttpPost]
        public IActionResult Crear(CuentaAhorroVM model)
        {
            if (ModelState.IsValid)
            {
                var resultado = _cuentaAhorroService.CrearCuentaAhorro(model.ToCuentaAhorro());

                if (resultado == 1)
                {
                    TempData["Message"] = "Guardado con éxito";
                    return RedirectToAction("Gestionar", new { idcliente = model.Cliente.IdCliente });
                }
                else
                {
                    TempData["Message"] = "Error al guardar";
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Editar(int idcuenta)
        {
            var cuenta = _cuentaAhorroService.ObtenerCuentaAhorro(idcuenta).ToCuentaAhorroVM();
            var cliente = _clienteService.ObtenerCliente(cuenta.Cliente.IdCliente).ToClienteVM();
            cuenta.Cliente = cliente;
            return View(cuenta);
        }

        [HttpPost]
        public IActionResult Editar(CuentaAhorroVM model)
        {
            if (ModelState.IsValid)
            {
                var resultado = _cuentaAhorroService.EditarCuentaAhorro(model.ToCuentaAhorro());

                if (resultado == 1)
                {
                    TempData["Message"] = "Guardado con éxito";
                    return RedirectToAction("Gestionar", new { idcliente = model.Cliente.IdCliente });
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
