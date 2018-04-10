CsoundSynthesizer>
<CsOptions>
; Select audio/midi flags here according to platform
-odac     ;;;RT audio out
;-iadc    ;;;uncomment -iadc if RT audio input is needed too
; For Non-realtime ouput leave only the line below:
; -o abs.wav -W ;;; for file output any platform
</CsOptions>
<CsInstruments>

sr = 44100
ksmps = 32
nchnls = 2
0dbfs  = 1

instr 1

kFreq init 440

kInter readk "Intervalle.txt", 8, 0.5


kDZeit = 0.5
kAZeit init 0

kNZeit times


if ( kNZeit > (kAZeit + kDZeit )) then
kAZeit = kNZeit
kFrew = 0

while (( kFrew < 110 ) || ( kFrew > 1100 )) do
kRicht random 0, 1
kStep random 1, 16

kZ1 = (kInter + 1 ) / kInter
kZ2 = (2*kRicht)-1
kZ3 = exp(log(kZ1)*kStep)
kZ4 = kZ3*kZ2
kFrew = kFreq * (1+kZ4)
od

kFreq = kFrew
endif

printks "Freeq= %f \n", 0.5, kFrew

kamp = 1
asig oscil kamp, kFreq
     outs asig,asig


dumpk  kFreq, "Frequenzen.txt", 8, 0.5

endin

</CsInstruments>
<CsScore>

i 1 0 12
; i 2 0 90
e

</CsScore>
</CsoundSynthesizer>
