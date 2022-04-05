using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEXimdone.Models
{
    public class PredictorData
    {
        public float route { get; set; }
        public float milepoint { get; set; }
        public float lat_utm_y { get; set; }
        public float long_utm_x { get; set; }
        public float pedestrian_involved { get; set; }
        public float motorcycle_involved { get; set; }
        public float improper_restraint { get; set; }
        public float dui { get; set; }
        public float intersection_related { get; set; }
        public float overturn_rollover { get; set; }
        public float teenage_driver_involved { get; set; }
        public float older_driver_involved { get; set; }
        public float night_dark_condition { get; set; }
        public float single_vehicle { get; set; }
        public float distracted_driving { get; set; }
        public float county_name_Other { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                 route, milepoint, lat_utm_y, long_utm_x, pedestrian_involved,
                 motorcycle_involved, improper_restraint, dui, intersection_related,
                 overturn_rollover, teenage_driver_involved, older_driver_involved,
                 night_dark_condition, single_vehicle, distracted_driving,
                 county_name_Other
            };
            int[] dimensions = new int[] { 1, 16 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}
