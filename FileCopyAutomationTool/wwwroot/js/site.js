// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', () => {
    var selectSource = document.getElementById('selectSource');
    var sourcePicker = document.getElementById('sourcePicker');
    var sourcePath = document.getElementById('SourcePath');
    var selectDestination = document.getElementById('selectDestination');
    var destinationPicker = document.getElementById('destinationPicker');
    var destinationPath = document.getElementById('DestinationPath');
    var fileCopyForm = document.getElementById('fileCopyForm');

    // Check if elements exist before adding event listeners
    if (selectSource && sourcePicker && sourcePath) {
        selectSource.addEventListener('click', () => {
            sourcePicker.click(); // Trigger the hidden file input
        });

        sourcePicker.addEventListener('change', (event) => {
            var files = event.target.files;
            if (files.length > 0) {
                var folderPath = files[0].webkitRelativePath.split('/')[0];
                sourcePath.value = folderPath; // Set folder path
            } else {
                console.error('No folder selected');
            }
        });
    }

    if (selectDestination && destinationPicker && destinationPath) {
        selectDestination.addEventListener('click', () => {
            destinationPicker.click(); // Trigger the hidden file input
        });

        destinationPicker.addEventListener('change', (event) => {
            var files = event.target.files;
            if (files.length > 0) {
                var folderPath = files[0].webkitRelativePath.split('/')[0];
                destinationPath.value = folderPath; // Set folder path
            } else {
                console.error('No folder selected');
            }
        });
    }

    // Automatically submit the form if query parameters are present
    if (fileCopyForm) {
        var urlParams = new URLSearchParams(window.location.search);
        var source = urlParams.get('source');
        var destination = urlParams.get('destination');

        if (source && destination) {
            sourcePath.value = source;
            destinationPath.value = destination;
            fileCopyForm.submit();
        }
    }
});

