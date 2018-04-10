<CsoundSynthesizer>
<CsOptions>
; Select audio/midi flags here according to platform
-odac     ;;;realtime audio out
;-iadc    ;;;uncomment -iadc if realtime audio input is needed too
; For Non-realtime ouput leave only the line below:
-o oscil.wav -W ;;; for file output any platform
</CsOptions>
<CsInstruments>

sr = 44100
ksmps = 32
nchnls = 2
0dbfs  = 1

gkFreq = 440


FLpanel "Oegel1 Knob", 500, 400, 50, 50
    ; Minimum value output by the knob
    imin = 1
    ; Maximum value output by the knob
    imax = 6
    ; Logarithmic type knob selected
    iexp = -1
    ; Knob graphic type (1=3D knob)
    itype = 1 
    ; Display handle (-1=not used)
    idisp = -1
    ; Width of the knob in pixels
    iwidth = 70
    ; Distance of the left edge of the knob 
    ; from the left edge of the panel
    ix = 70
    ; Distance of the top edge of the knob 
    ; from the top of the panel
    iy = 75

    gkMaxStep, ihandle FLknob "Maximaler Step", imin, imax, iexp, itype, idisp, iwidth, ix, iy
    ix = 200
    gkMaxInter, ihandle FLknob "Maximale Dissonanz", imin, imax, iexp, itype, idisp, iwidth, ix, iy

    ; gkplay, ihb1 FLbutton "@>", ion, ioff, itype, iwidth, iheight, ix, iy, iopcode, 1, istarttim, idur, 1
    gkrecord, ihb1 FLbutton "Record", 1, 0, 2, 150, 50, 50, 200, 0, 2, 1, 1
; End of panel contents
FLpanelEnd
; Run the widget thread!
FLrun


instr 3
    dumpk  gkFreq, "Test.txt", 8, 1
    printk 0.5, gkFreq
endin

instr 2


ktroll init 1

if ( ktroll == 1 ) then

if gkrecord == 1 then
    printk 0.3, gkrecord
    turnon 3
endif
    
if gkrecord == 0 then
    printk 0.3, gkrecord
    turnoff2 3, 0, 0
endif

ktroll = 0
endif


endin

instr 1

kamp = 1

kDZeit = 0.5
kAZeit init 0
kcps init 440


kNZeit times

if ( kNZeit > (kAZeit + kDZeit )) then
kAZeit = kNZeit
kcpsv = 0

while (( kcpsv < 110 ) || ( kcpsv > 1100 )) do
kTest1 random -1, 1
kFaktor random 1, int( gkMaxStep )
kMulti random 1, int (gkMaxInter )

kMultiEr = ( kMulti + 1) / (kMulti)
kMultiEr2 = exp(log(kMultiEr) * kFaktor)

if kTest1 < 0 then
kcpsv = kcps * kMultiEr2
endif


if kTest1 > 0 then
kcpsv = kcps / kMultiEr2
endif

od

kcps = kcpsv
endif

asig oscil kamp, kcps
gkFreq = kcps
     outs asig,asig

endin

</CsInstruments>
<CsScore>

i 1 0 60
i 3 0 60
e
</CsScore>
</CsoundSynthesizer>
