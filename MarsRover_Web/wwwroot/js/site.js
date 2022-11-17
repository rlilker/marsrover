'use strict';

class Output {
    static showOutput(text) {
        this.#showOutputHtml(`<p class="white">${this.#cleanseOutput(text)}</p>`);
        
    }
    static showError(text) {
        this.#showOutputHtml(`<p class="red">${this.#cleanseOutput(text)}</p>`);
    }
    static clear() {
        document.getElementById("output").innerText = "";
    }

    static #showOutputHtml(outputHtml) {
        var outputWindow = document.getElementById("output");
        outputWindow.insertAdjacentHTML('beforeend', outputHtml);
    }
    static #cleanseOutput(text) {
        return `${text.replace(/\n/, "<br />")}`;
    }
}

class Position {

    static #GRIDSIZE = 5;
    static #CELLSIZE = 100;
    static #ROVERSIZE = 40;

    static calculatePositionX(position) {
        var startingCoords = this.#GRIDSIZE * this.#CELLSIZE; //500
        var positionalAdjustment = this.#adjustmentForPosition(position, 0); //
        var roverAdjustment = this.#adjustmentForCellAndRoverSize();

        return startingCoords - positionalAdjustment + roverAdjustment;
    }

    static calculatePositionY(position) {
        var startingCoords =  0;
        var positionalAdjustment = this.#adjustmentForPosition(position, 1);
        var roverAdjustment = this.#adjustmentForCellAndRoverSize();

        return startingCoords + positionalAdjustment + roverAdjustment;
    }
    
    //Ideally I'd' like something an Enum, Class or Token here to make it more readable but I ran out of time
    static orientation(orientation) {
        switch(orientation) {
            case 0:
            default:
                return 'rotate(0deg)';
            case 1:
                return 'rotate(90deg)';
            case 2:
                return 'rotate(180deg)';
            case 3:
                return 'rotate(270deg)';
        }
    }

    static #adjustmentForPosition(position, modifier) {
        return (position - modifier) * this.#CELLSIZE;
    }

    static #adjustmentForCellAndRoverSize() {
        return (this.#CELLSIZE  / 2) - (this.#ROVERSIZE / 2);
    }
}

class Plateau {

    #GRIDSIZE = 5;
    #CELLSIZE = 100;
    #ROVERSIZE = 20;

    #plateauElem;
    #rovers = {};
    
    constructor() {
        this.#plateauElem = document.getElementById('plateau');
        let existingRovers = document.getElementsByClassName('rover');
        for (let roverElem of existingRovers) {
            this.#rovers[roverElem.id] = roverElem;
        }    }
    
    clear() {
        for (let roverKey in this.#rovers) {
            this.#rovers[roverKey].remove();
        }
        this.#rovers = {};
    }

    moveRover(rover) {
        if (!this.#rovers[rover.name]) {
            this.#addRover(rover);
        }
        let roverElem = this.#rovers[rover.name];
        roverElem.style.top = Position.calculatePositionY(rover.position.y) + "px";
        roverElem.style.left = Position.calculatePositionX(rover.position.x) + "px";
        roverElem.style.transform = Position.orientation(rover.orientation);
    }
    
    #addRover(rover) {
        const roverElem = document.createElement('div');
        roverElem.className = 'rover';
        roverElem.id = rover.name;
        this.#plateauElem.appendChild(roverElem);

        this.#rovers[rover.name] = roverElem;
    }
}

class Rover {
    
    #name;
    #position;
    #orientation;

    constructor(name, position, orientation) {
        this.#name = name;
        this.#position = position;
        this.#orientation = orientation;
    }

    get name() {
        return this.#name.replace(/ /, '');
    }

    get position() {
        return this.#position;
    }

    get orientation() {
        return this.#orientation;
    }
}

class RoverMovement {
    
    #plateau;
    #rover;

    constructor(plateau, rover) {
        this.#plateau = plateau;
        this.#rover = rover;
    }

    execute() {
        this.#plateau.moveRover(this.#rover);
    }
}

async function AJAXSubmit (oFormElement) {

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
};

const moveRover = function(plateau, resultItem) {
        if (resultItem.success) {
            let rover = new Rover(resultItem.value.name, resultItem.value.position, resultItem.value.orientation);
            let roverMovement = new RoverMovement(plateau, rover);
            roverMovement.execute();

            Output.showOutput(resultItem.message);
        } else {
            Output.showError(`Error: ${resultItem.message}`);
        }
};

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
