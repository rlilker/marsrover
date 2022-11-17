export default class RoverMovement {
    
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