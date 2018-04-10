void setup(){
  size(700, 400);
  colorMode(HSB);
  int hohe = height;
  int breite = width;
  color farbe = color(0,0,0);
  int w, l, ll, lll;
  lll = 0;
  String[] lines = loadStrings("Test.txt");
  l = breite/lines.length;
println("there are " + lines.length + " lines");
for (int i = 0 ; i < lines.length; i++) {
  ll = l * (i+1);
  w = int(lines[i]);
  w = w * 10;
  if (w>250){
    w = 250;
  }
  farbe = color(w,200,200);
  fill(farbe);
  rect(lll,0,ll,hohe);
  lll = ll;
  
  save("Bild.png");
}

}

void draw(){
    
}
