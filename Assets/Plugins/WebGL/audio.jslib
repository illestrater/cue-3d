mergeInto(LibraryManager.library, {
    analyser: null,
    frequencyData: null,
    getFFT: null,
    audioElement: null,
    audioSrc: null,
    play__deps: [
        'audioElement',
        'audioSrc',
        'analyser',
        'frequencyData',
        'getFFT'
    ],
    play: function () {
        var audioCtx = new (window.AudioContext || window.webkitAudioContext)();
        _audioElement = document.getElementById('audioElement');
        _audioSrc = audioCtx.createMediaElementSource(_audioElement);
        _analyser = audioCtx.createAnalyser();
        _audioSrc.connect(_analyser);
        _audioSrc.connect(audioCtx.destination);
        _frequencyData = new Uint8Array(22);
        _getFFT = null;
        _audioElement.play();
    },
    renderChart__deps: [
        'renderChart',
        'analyser',
        'frequencyData',
        'getFFT'
    ],
    renderChart: function () {
        requestAnimationFrame(_renderChart);
        _analyser.getByteFrequencyData(_frequencyData);
        _getFFT = _frequencyData;
    },
    stop__deps: ['audioElement'],
    stop: function () {
        _audioElement.stop();
    },
    hackWebGLKeyboard: function () {
        var webGLInput = document.getElementById('m');
        for (var i in JSEvents.eventHandlers) {
            var event = JSEvents.eventHandlers[i];
            if (event.eventTypeString == 'keydown' || event.eventTypeString == 'keypress' || event.eventTypeString == 'keyup') {
                webGLInput.addEventListener(event.eventTypeString, event.eventListenerFunc, event.useCapture);
                window.removeEventListener(event.eventTypeString, event.eventListenerFunc, event.useCapture);
            }
        }
    },
    getFrequencyData__deps: ['getFFT'],
    getFrequencyData: function () {
        var buffer = _malloc(lengthBytesUTF8(_getFFT.toString()) + 1);
        writeStringToMemory(_getFFT.toString(), buffer);
        return buffer;
    }
});
