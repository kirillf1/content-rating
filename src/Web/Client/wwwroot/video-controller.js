

function play(className) {
	$('.' + className)[0].contentWindow.postMessage('{"event":"command","func":"' + 'playVideo' + '","args":""}', '*');
};
function stop(className) {
	$('.' + className)[0].contentWindow.postMessage('{"event":"command","func":"' + 'stopVideo' + '","args":""}', '*');
};

function pause(className) {
	$('.' + className)[0].contentWindow.postMessage('{"event":"command","func":"' + 'pauseVideo' + '","args":""}', '*');
};
function playAudio(className) {
	
	var audio = $('.' + className)[0];
	audio.play();
};
function stopAudio(className) {
	var audio = $('.' + className)[0];
	audio.pause();
	audio.currentTime = 0;
};

function pauseAudio(className) {
	var audio = $('.' + className)[0];
	audio.pause();
};