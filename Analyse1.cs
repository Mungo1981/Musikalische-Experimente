<CsoundSynthesizer>
<CsOptions>
</CsOptions>
<CsInstruments>

sr = 44100
ksmps = 10
nchnls = 2
0dbfs  = 1


FLpanel "Frequency Knob", 600, 300, 50, 50
    ; Minimum value output by the knob
    imin = 2
    ; Maximum value output by the knob
    imax = 100
    ; Logarithmic type knob selected
    iexp = 0
    ; Knob graphic type (1=3D knob)
    itype = 1 
    ; Display handle (-1=not used)
    idisp = 2
    ; Width of the knob in pixels
    iwidth = 70
    ; Distance of the left edge of the knob 
    ; from the left edge of the panel
    ix = 60
    ; Distance of the top edge of the knob 
    ; from the top of the panel
    iy = 40

    gkfreq, ihandle FLknob "MaxIntervall", imin, imax, iexp, itype, idisp, iwidth, ix, iy

        ; Minimum value output by the knob
    imin = 3
    ; Maximum value output by the knob
    imax = 1000
    ; Logarithmic type knob selected
    iexp = -1
    ; Knob graphic type (1=3D knob)
    itype = 1 
    ; Display handle (-1=not used)
    idisp = 2
    ; Width of the knob in pixels
    iwidth = 70
    ; Distance of the left edge of the knob 
    ; from the left edge of the panel
    ix = 180
    ; Distance of the top edge of the knob 
    ; from the top of the panel
    iy = 40

    gkfreq, ihandle FLknob "MaxSprung", imin, imax, iexp, itype, idisp, iwidth, ix, iy
; Minimum value output by counter
    imin = 110
    ; Maximum value output by counter
    imax = 11000
    ; Single arrow step size (semitones)
    istep1 = 10
    ; Double arrow step size (octave)
    istep2 = 100
    ; Counter type (1=double arrow counter)
    itype = 1
    ; Width of the counter in pixels
    iwidth = 200
    ; Height of the counter in pixels
    iheight = 30
    ; Distance of the left edge of the counter
    ; from the left edge of the panel
    ix = 300
    ; Distance of the top edge of the counter
    ; from the top edge of the panel
    iy = 45
    ; Score event type (-1=ignored)
    iopcode = -1

    gkBezug, ihandle FLcount "Tonaler Bezug in Hz", imin, imax, istep1, istep2, itype, iwidth, iheight, ix, iy, iopcode, 1, 0, 1

    gkTonal, ihb2 FLbutton "Tonal", 1, 0, 2, 150, 50, 300, 150, 0, 1, 1, 1
    gkanalyse, ihb1 FLbutton "Analyse", 1, 0, 1, 150, 50, 75, 150, 0, 1, 1, 1
    gkEnde, ihb1 FLbutton "Ende", 1, 0, 1, 150, 50, 75, 225, 0, 3, 1, 1
    ; gkplay, ihb1 FLbutton "@>", ion, ioff, itype, iwidth, iheight, ix, iy, iopcode, 1, istarttim, idur, 1

; End of panel contents
FLpanelEnd
; Run the widget thread!
FLrun


instr 3
turnoff2 2, 0, 0
turnoff2 3, 0, 0
endin


instr 1

kAFreq init 440

kFreq readk "Test.txt", 7, 0
printk 0., kFreq

;#####
if gkTonal == 0 then
kF = kAFreq / kFreq
else
kF = gkBezug / kFreq
endif

if kF < 1 then
kF = 1 / kF

endif

kAFreq = kFreq

kB1 = 0
kT = 1
kST = 480
kSD = 1000
kSG = 0.7
while ( ( kT < kST ) && ( kB1 == 0 )) do
kTT = ( kT + 1) / kT
kSE = 1
while ( (kSE < kSD ) && (kB1 == 0)) do
kSE = kSE + 1

; kkk#####
kZ = exp(log(kTT)*kSE)
kZ2= kZ*(1+kSG)
kZ1 = kZ * (1/kSG)
if ( kZ1 < kF ) && ( kF < kZ2) then
kB1 = 1
endif
; kkk#####
od

kT = kT + 1
od

printk 0, kT
 dumpk  kT, "Intervalle.txt", 7, 0

if kFreq == 0 then
    turnoff
endif

endin

instr 2
endin

</CsInstruments>
<CsScore>

i 2 0 60000
e
</CsScore>
</CsoundSynthesizer>
