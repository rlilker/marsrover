import Position from '../src/position';

describe('Position', () => {
  
  let xCoords = [
    [1, 30],
    [2, 130],
    [3, 230],
    [4, 330],
    [5, 430],
  ]  

  test.each(xCoords)('X Coordinate %p expects %p', (x, expected) => {

    let position = Position.calculatePositionX(x);

    expect(position).toBe(expected);
  });

  let yCoords = [
    [1, 430],
    [2, 330],
    [3, 230],
    [4, 130],
    [5, 30],
  ]

  test.each(yCoords)('X Coordinate %p expects %p', (y, expected) => {

    let position = Position.calculatePositionY(y);

    expect(position).toBe(expected);
  });

  let orientationIndices = [
    [0, 'rotate(0deg)'],
    [1, 'rotate(90deg)'],
    [2, 'rotate(180deg)'],
    [3, 'rotate(270deg)'],
    [4, 'rotate(0deg)']
  ]
  test.each(orientationIndices)('Orientation Index %p expects %p', (o, expected) => {

    let rotateDegrees = Position.orientation(o);

    expect(rotateDegrees).toBe(expected);
  });
});