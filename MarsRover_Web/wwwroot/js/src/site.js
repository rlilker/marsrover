import Output from "./output.js";
import AJAXSubmit from "./upload.js";

document.addEventListener("DOMContentLoaded", () => {
    setupFileUpload();
});

function setupFileUpload() {
    Output.showOutput("Setting up File Upload...");


    document.getElementById("FileUpload").addEventListener("submit", (event) => {
        event.preventDefault(); //stop the page reloading on form submit
        
        var caller = event.target || event.srcElement;

        if (document.getElementById('formFile').value != "") {
            AJAXSubmit(caller);
        }
    });

    Output.showOutput("File Upload setup complete!");
}