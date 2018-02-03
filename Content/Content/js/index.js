var inputOpened = false;

function openInput() {
  if (inputOpened) {
    TweenMax.to(".myinput", 0.5, {
      width: 0,left:55
    });
    TweenMax.to(".search", 0.5, {
      rotation: 0,
      left:55
    });
    inputOpened = false;
  } else {
    TweenMax.to(".myinput", 0.5, {
      width: 250,
      left:0
    });
    TweenMax.to(".search", 0.5, {
      rotation: 360,
      left:110,
      onComplete: focusme
    });
    inputOpened = true;
  }
}

function focusme(){
  document.querySelector('.myinput').focus()
}