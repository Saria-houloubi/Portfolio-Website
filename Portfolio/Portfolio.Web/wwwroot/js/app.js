"use strict";
Blazor.start().then(function (e) {
    fillAnimatedBg("animated_bg_shapes", 30);
});
/**
 * Fills the animated background with shapes
 * @param id of the element to append to
 * @param maxDensity the max density to create
 */
function fillAnimatedBg(id, maxDensity) {
    var animatedBg = document.getElementById(id);
    var shapes = ["circle", "triangle", "square"];
    var density = window.innerWidth * 0.09;
    density = density > 30 ? 30 : density;
    for (var i = 0; i < density; i++) {
        var _shape = createShape(shapes[getRandomBetween(0, 3)], window.innerHeight * 0.03);
        _shape.style.top = getRandomBetween(1, window.innerHeight) + "px";
        _shape.style.left = getRandomBetween(1, window.innerWidth) + "px";
        animatedBg.appendChild(_shape);
    }
}
/**
 * Creates a shape and returns html element
 * @param type the type of polygone to create
 * @param maxSizePx the max size of element based on screen size
 */
function createShape(type, maxSizePx) {
    switch (type) {
        case "circle":
            return createCircleSpan(maxSizePx);
        case "triangle":
            return createTriangleSpan(maxSizePx);
        case "square":
            return createSquareSpan(maxSizePx);
        default:
    }
    throw new Error("Type " + type + " is not found to create.");
}
/**
 * Creates a circle span
 * */
function createCircleSpan(maxSizePx) {
    //Create the element
    var span = document.createElement("span");
    //set the size from a random value  1 to mazSize
    var size = getRandomBetween(1, maxSizePx);
    span.style.width = size + "px";
    span.style.height = size + "px";
    span.style.borderRadius = "50%";
    return span;
}
/**
 * Creates a Trinagle span
 * */
function createTriangleSpan(maxSizePx) {
    //Create the element
    var span = document.createElement("span");
    //set the size from a random value  1 to mazSize
    var size = getRandomBetween(1, maxSizePx);
    var base = getRandomBetween(1, 4);
    span.style.width = "$0px";
    span.style.height = "$0px";
    span.style.borderLeft = base === 1 ? size * 2 + "px solid  var(--main-fg-dark-color)" : size + "px solid transparent";
    span.style.borderRight = base === 2 ? size * 2 + "px solid  var(--main-fg-dark-color)" : size + "px solid transparent";
    span.style.borderBottom = base === 3 ? size * 2 + "px solid  var(--main-fg-dark-color)" : size + "px solid transparent";
    span.style.borderTop = base === 4 ? size * 2 + "px solid  var(--main-fg-dark-color)" : size + "px solid transparent";
    span.style.backgroundColor = "transparent";
    return span;
}
/**
 * Creates a Square span
 * */
function createSquareSpan(maxSizePx) {
    //Create the element
    var span = document.createElement("span");
    //set the size from a random value  1 to mazSize
    var size = getRandomBetween(1, maxSizePx);
    span.style.width = size + "px";
    span.style.height = size + "px";
    return span;
}
/**
 * Gets a random value between two values
 * @param from
 * @param to
 */
function getRandomBetween(from, to) {
    return Math.floor(Math.random() * (to - from) + from);
}
