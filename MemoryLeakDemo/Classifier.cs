using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Security;
using Microsoft.ML.Transforms.Image;
using Microsoft.ML.Transforms.Onnx;

namespace MemoryLeakDemo
{
    class Classifier
    {
        private MLContext ML;
        private ITransformer model;
        private string inputName = "input_2";
        private string outputName = "dense_2";

        public Classifier()
        {
            ML = new MLContext();

            var imageLoader = ML.Transforms.LoadImages(inputName, string.Empty, nameof(InputData.ImagePath));
            var imageResizer = ML.Transforms.ResizeImages(inputName, 320, 240);
            var pixelExtractor = ML.Transforms.ExtractPixels(inputName, interleavePixelColors: true, colorsToExtract: ImagePixelExtractingEstimator.ColorBits.Red);
            var onnxEstimator = ML.Transforms.ApplyOnnxModel(new string[] { outputName }, new string[] { inputName }, "Model.onnx");

            var pipeline = imageLoader.Append(imageResizer).Append(pixelExtractor).Append(onnxEstimator);

            var data = ML.Data.LoadFromEnumerable(new List<InputData>());
            model = pipeline.Fit(data);
        }


        public void ClassifyImage(string imagePath)
        {
            var inputData = new InputData()
            {
                ImagePath = imagePath
            };

            var data = ML.Data.LoadFromEnumerable(new List<InputData>() { inputData });

            var result = model.Transform(data);

            //IEnumerable<float[]> probs = result.GetColumn<float[]>(outputName);
        }
    }

    class InputData
    {
        public string ImagePath;
    }
}
