

function playAudio(className) {
	var audio = $('.' + className);
	audio.play();
};
function stopAudio(className) {
	var audio = $('.' + className);
	audio.pause();
	audio.currentTime = 0;
};

function pauseAudio(className) {
	var audio = $('.' + className);
	audio.pause();
};