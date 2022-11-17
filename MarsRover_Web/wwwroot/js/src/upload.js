import Output from "./output.js";
import Plateau from "./plateau.js";
import Rover from "./rover.js";
import RoverMovement from "./roverMovement.js";

export default async function AJAXSubmit (oFormElement) {

    const timeDelay = 500;

    const formData = new FormData(oFormElement);

    try {

        Output.showOutput("Uploading file...");

        const response = await fetch(oFormElement.action, {
            method: 'POST',
            body: formData
        })
        .then((response) => response.json())
        .then((result) => {
            Output.showOutput("File uploaded!");

            if (result.success) {
                moveRovers(result.value, timeDelay);
            } else {
                if (result.message) {
                    Output.showError(`Error: ${result.message}`);
                } else {
                    Output.showError("Error: No results returned");        
                }
            }
        });;
    } catch (error) {
        Output.showError(`Error: ${error.message}`);
    }
}

const moveRovers = function (roverResult, timeDelay) {
    let plateau = new Plateau();
    
    plateau.clear();
    Output.clear();

    roverResult.forEach(function(resultItem, index) { 
        setTimeout(function(){
            moveRover(plateau, resultItem);
        }, timeDelay * (index + 1));
    });
}

const moveRover = function(plateau, resultItem) {
        if (resultItem.success) {
            let rover = new Rover(resultItem.value.name, resultItem.value.position, resultItem.value.orientation);
            let roverMovement = new RoverMovement(plateau, rover);
            roverMovement.execute()

            Output.showOutput(resultItem.message);
        } else {
            Output.showError(`Error: ${resultItem.message}`);
        }
}