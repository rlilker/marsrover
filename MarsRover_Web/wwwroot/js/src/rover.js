export default class Rover {
    
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