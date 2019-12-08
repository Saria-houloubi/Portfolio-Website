window.onload = function(e) {
    createBubble()
}



function createBubble () {
    //Get all the elements that we need to add bubles to
    var elements = document.querySelectorAll('[data-bubles-density]')

    //Loop over each element
    for(var i =0; i< elements.length;i++){

        
        var density = elements[i].getAttribute('data-bubles-density') * 100;

        for (var d = 0 ; d < density ;d++){

            var size = GetRandomBetween(1,8);
            var top = GetRandomBetween(80,100);
            var left = GetRandomBetween(0,100);


            elements[i].innerHTML+= '<span class="buble" style="left: '+ left+'%; top: '+ top+'%; height: '+ size+'px; width: '+ size+'px"></span>'
        }

    }
    


}
function GetRandomBetween(from ,to){
    return Math.floor(Math.random() * (to - from) + from)
}