namespace FileCopyAutomationTool.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    // using System.IO;
    using System.IO;
    using FileCopyAutomationTool.Models;

    public class FileCopyController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // return View();
            var model = new FileCopyModel(); // Initialize the model
            return View(model);
        }

        [HttpPost]
        public IActionResult CopyFiles(FileCopyModel model)
        {
            if (model == null)
            {
                model = new FileCopyModel(); // Initialize model if null
            }

            if (string.IsNullOrEmpty(model.SourcePath) || string.IsNullOrEmpty(model.DestinationPath))
            {
                model.ResultMessage = "Please select a source folder and a destination folder.";
                return View("Index", model); // Pass the model back to the view
            }

            // Ensure the source path is absolute
            string sourcePath = Path.GetFullPath(model.SourcePath);
            string destinationPath = Path.GetFullPath(model.DestinationPath);
            // Print the paths to debug
            Console.WriteLine($"Source Path: {sourcePath}");
            Console.WriteLine($"Destination Path: {destinationPath}");

            // Ensure source folder exists
            if (!Directory.Exists(sourcePath))
            {
                model.ResultMessage = "Source folder does not exist.";
                return View("Index", model); // Return with error message if source folder doesn't exist
            }

            // Ensure destination folder exists
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            try
            {
                // Copy each file from the source folder to the destination folder
                var sourceFolder = Directory.GetFiles(sourcePath);

                foreach (var file in sourceFolder)
                {
                    var fileName = Path.GetFileName(file);
                    var destinationFilePath = Path.Combine(destinationPath, fileName);

                    using (var stream = new FileStream(destinationFilePath, FileMode.Create))
                    {
                        var sourceFileStream = new FileStream(file, FileMode.Open);
                        sourceFileStream.CopyTo(stream);
                        sourceFileStream.Close();
                    }
                }

                model.ResultMessage = "Files copied successfully!";
            }
            catch (Exception ex)
            {
                model.ResultMessage = $"Error: {ex.Message}";
            }

            return View("Index", model); // Pass the model with the result message back to the view
        }


        //[HttpPost]
        //public IActionResult CopyFiles(string sourcePath, string destinationPath)
        //{
        //    if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destinationPath))
        //    {
        //        ViewData["ResultMessage"] = "Source and Destination paths cannot be empty.";
        //        return View("Index");
        //    }

        //    if (!Directory.Exists(sourcePath))
        //    {
        //        ViewData["ResultMessage"] = "Invalid Source Path.";
        //        return View("Index");
        //    }

        //    if (!Directory.Exists(destinationPath))
        //    {
        //        ViewData["ResultMessage"] = "Invalid Destination Path.";
        //        return View("Index");
        //    }

        //    try
        //    {
        //        // Copy files logic
        //        string[] files = Directory.GetFiles(sourcePath);

        //        foreach (var file in files)
        //        {
        //            var fileName = Path.GetFileName(file);
        //            var destFile = Path.Combine(destinationPath, fileName);
        //            System.IO.File.Copy(file, destFile, true);
        //        }

        //        ViewData["ResultMessage"] = "Files copied successfully!";
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["ResultMessage"] = $"Error: {ex.Message}";
        //    }

        //    return View("Index");
        //}


        //    [HttpPost]
        //    public IActionResult CopyFiles(FileCopyModel model)
        //    {
        //        if (Directory.Exists(model.SourcePath) && Directory.Exists(model.DestinationPath))
        //        {

        //            try
        //            {
        //                // Get all files in the source directory
        //                string[] files = Directory.GetFiles(model.SourcePath);

        //                foreach (string file in files)
        //                {
        //                    // Get file name
        //                    string fileName = Path.GetFileName(file);

        //                    // Define destination file path
        //                    string destFile = Path.Combine(model.DestinationPath, fileName);

        //                    // Copy file to destination
        //                    System.IO.File.Copy(file, destFile, true);
        //                }

        //                model.ResultMessage = "Files copied successfully!";
        //            }
        //            catch (Exception ex)
        //            {
        //                model.ResultMessage = $"Error: {ex.Message}";
        //            }
        //        }
        //        else
        //        {
        //            model.ResultMessage = "Source or destination path does not exist!";
        //        }

        //        return View("Index", model); // Pass model back to the view
        //    }
        //}
    }
}
