using INTEXimdone.Models;
using INTEXimdone.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace INTEXimdone.Controllers
{
    public class InferenceController : Controller
    {
        private InferenceSession _session;

        public InferenceController(InferenceSession session)
        {
            _session = session;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Calculator()
        {
            return View();
        }

        [HttpPost] //Do I need this?
        public ActionResult Score(PredictionViewModel data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.PredictorData.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            ViewBag.prediction = score.First();
            var predictionView = new PredictionViewModel { PredictorData = data.PredictorData, Prediction = prediction };
            result.Dispose();
            return View("Calculator", predictionView);
            //return Ok(prediction); // Change this
        }
    }
}
