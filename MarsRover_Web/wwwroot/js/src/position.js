export default class Position {

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