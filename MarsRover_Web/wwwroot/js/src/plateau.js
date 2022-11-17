import Position from "./position";

export default class Plateau {

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
        };
    }
    
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