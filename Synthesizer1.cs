<CsoundSynthesizer>
<CsOptions>
; Select audio/midi flags here according to platform
-odac     ;;;RT audio out
;-iadc    ;;;uncomment -iadc if RT audio input is needed too
; For Non-realtime ouput leave only the line below:
-o abs.wav -W ;;; for file output any platform
</CsOptions>
<CsInstruments>

sr = 44100
ksmps = 32
nchnls = 2

gkFreq2 = 440

instr 2
dumpk  gkFreq2, "FrequenzenAus.txt", 8, 1
endin

instr 1
a1   diskin "Test.wav", 1, 0, 1, 0, 32

fftin     pvsanal a1, 1024, 256, 2048, 0	;fft-analysis of the audio-signal
kfr, kamp pvspitch   fftin, 0.01

gkFreq2 = kfr * 4
; fftblur   pvscale fftin, p5					;scale


kFreq readk "FrequenzenEin.txt", 8, 0.5

printk 0.1, gkFreq2

kKontroll = kFreq / gkFreq2

fftblur   pvscale fftin, kKontroll					;scale

aout		pvsynth	fftblur; resynthesis

out aout

endin

</CsInstruments>
<CsScore>

i 1 0 180
i 2 0 180
e

</CsScore>
</CsoundSynthesizer>



