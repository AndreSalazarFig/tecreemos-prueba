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
    public class TransaccionController : Controller
    {
        readonly ICuentaAhorroService _cuentaAhorroService;
        readonly IClienteService _clienteService;
        readonly ITransaccionService _transaccionService;

        public TransaccionController(ICuentaAhorroService cuentaAhorroService, IClienteService clienteService, ITransaccionService transaccionService)
        {
            _cuentaAhorroService = cuentaAhorroService;
            _clienteService = clienteService;
            _transaccionService = transaccionService;
        }

        public IActionResult Historial(int idcuenta)
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
                TempData.Remove("Message");
            }

            var cuenta = _cuentaAhorroService.ObtenerCuentaAhorro(idcuenta).ToCuentaAhorroVM();
            var cliente = _clienteService.ObtenerCliente(cuenta.Cliente.IdCliente).ToClienteVM();

            ViewBag.NumeroCuenta = cuenta.NumeroCuenta;
            ViewBag.IdCliente = cliente.IdCliente;
            ViewBag.NombreCliente = $"{cliente.Nombre ?? ""} {cliente.ApellidoPaterno ?? ""} {cliente.ApellidoMaterno ?? ""}".Trim();

            var transacciones = _transaccionService.ObtenerTransacciones(idcuenta).ToHistorialTransaccionVM();

            return View(transacciones);
        }

        [HttpGet]
        public IActionResult Nueva(int idcuenta)
        {
            var cuenta = _cuentaAhorroService.ObtenerCuentaAhorro(idcuenta).ToCuentaAhorroVM();
            var cliente = _clienteService.ObtenerCliente(cuenta.Cliente.IdCliente).ToClienteVM();

            return View(new TransaccionVM()
            {
                IdCuentaAhorro = idcuenta,
                IdCliente = cliente.IdCliente,
                NombreCliente = $"{cliente.Nombre ?? ""} {cliente.ApellidoPaterno ?? ""} {cliente.ApellidoMaterno ?? ""}".Trim(),
                NumeroCuenta = cuenta.NumeroCuenta
            });
        }

        [HttpPost]
        public IActionResult Nueva(TransaccionVM model)
        {
            if (ModelState.IsValid)
            {
                var resultado = _transaccionService.CrearTransaccion(model.ToTransaccion());

                if (resultado > 0)
                {
                    TempData["Message"] = "Transacción realizada con éxito";
                    return RedirectToAction("Gestionar", "CuentaAhorro", new { idcliente = model.IdCliente });
                }
                else
                {
                    TempData["Message"] = "Error al realizar la transacción";
                }
            }

            return View(model);
        }
    }
}
