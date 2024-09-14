import './App.css';
import { useEffect, useState } from 'react';
import { checkPoint, fetchPolygons, savePolygon } from './services/polygon';
import Select from 'react-select';

const canvas = document.getElementById('canvas');
const context = canvas.getContext('2d');
canvas.width = canvas.offsetWidth;
canvas.height = canvas.offsetHeight;
const mouse = { x:0, y:0};
let draw = false;

let point = {
  x:0,
  y:0,
  number:0
};

let polygon = {
  name:"",
  points: [
    point
  ]
}

let pointPolygon = {
  point: point,
  polygon:polygon
}
let num = 0;

function App () {
  const [polygons,setPolygons] = useState([]);
  const[polygon,setPolygon] = useState(null);

  useEffect(() =>{
    const fetchData = async () => {
      var polygons = await fetchPolygons();

      setPolygons(polygons)
      
    };
    fetchData();
    const rep = () => {
      replay(polygon);
    }
    rep();

  },[polygon])

    return(
      <>
        <div className="m-1 row row-cols-lg-auto g-3 align-items-center">
          <div className="col-12">
            <button className="btn btn-primary" id="draw" onClick={drawStuff}>Рисовать</button>
          </div>
          <div className="col-12">
            <button className="btn btn-primary" onClick={drawPoint}>Выбрать точку</button>
          </div>
          <div className="col-12">
            <button id="btn-check" className="btn btn-secondary"  onClick={check}>Проверить</button>
          </div>
          <div className="col-12">
            <button className="btn btn-danger" onClick={clearCanvas}><img height='10'src="https://cdn.icon-icons.com/icons2/2066/PNG/96/eraser_icon_125292.png"/></button>
          </div>

        </div>
        <form onSubmit={save} className='m-2 w-25'>
              <input id="pName"className='form-control' required placeholder='Введите название' />
              <button type='submit' id="btn-save"className="mt-1 btn btn-success" >Сохранить</button>            
        </form>
        <div className='m-2'>
          <label>Загрузить полигон</label>
          <div className='mt-1 w-25'>
            <Select onChange={(e) => setPolygon(e)} options={polygons.map((p) => ({ value: p.points, label: p.name}))}/>
          </div>
        </div>
        
        
      </>
    )
}

function drawPoint(){
  canvas.addEventListener("mousedown", function(e){
    document.getElementById('btn-check').disabled=false;
    context.beginPath();
    context.arc(e.clientX,e.clientY,1,0,Math.PI*2);
    context.fill();
    point = {x:e.clientX,
             y:e.clientY};
  },{ once: true });
  
  return;
}
 
function drawStuff() {
  document.getElementById('draw').disabled=true;
  // нажатие мыши
  canvas.addEventListener("mousedown", mouseDown,{ once: true });
  // перемещение мыши
  canvas.addEventListener("mousemove", mouseMove);
  // отпускаем мышь
  canvas.addEventListener("mouseup", mouseUp,{ once: true });
  return;
  
}

function check() {
  pointPolygon.point = point;
  pointPolygon.polygon = polygon;
  checkPoint(JSON.stringify(pointPolygon));
}

function save() {
  var polygonName = document.getElementById('pName').value;
  if(polygonName !='')
  {
    polygon.name=polygonName;
    savePolygon(JSON.stringify(polygon));
    alert('Полигон ' + polygonName + ' успешно сохранен');
  }
  else
  {
    alert("Введите название полигона");
  }
}

const replay = (points) => {
  if(points!=null)
  {
    polygon.points=[];
    context.beginPath();
    clearCanvas();
    points.value.sort((a, b) => parseFloat(a.number) - parseFloat(b.number));
    let startX = points.value[1].x;
    let startY=points.value[1].y;
    let endX = points.value[points.value.length-2].x;
    let endY = points.value[points.value.length-2].y;
    
    for(let i=1;i<points.value.length;i++)
    {
      var x =points.value[i].x;
      var y =points.value[i].y;
      delete points.value[i].id;

      polygon.points.push(points.value[i]);
      
      context.lineTo(x - canvas.offsetLeft, y - canvas.offsetLeft);

      context.stroke();
      context.beginPath();
      context.moveTo(x - canvas.offsetLeft, y - canvas.offsetLeft);
      draw = false;  
    }
    context.moveTo(endX - canvas.offsetLeft, endY - canvas.offsetLeft);
    context.lineTo(startX - canvas.offsetLeft, startY - canvas.offsetLeft);
    context.stroke();
  }
}

function mouseMove(e){
  if(draw==true){
    num = num+1;
    point = {x:e.clientX,
             y:e.clientY,
             number:num};
    polygon.points.push(point)    
    mouse.x = e.pageX - this.offsetLeft;
    mouse.y = e.pageY - this.offsetTop;
    context.lineTo(mouse.x, mouse.y);
    context.stroke();
  }
}

function mouseDown(e){
  mouse.x = e.pageX - this.offsetLeft;
  mouse.y = e.pageY - this.offsetTop;
  draw = true;
  context.beginPath();
  context.moveTo(mouse.x, mouse.y);
}


function mouseUp(e){
  mouse.x = e.pageX - this.offsetLeft;
  mouse.y = e.pageY - this.offsetTop;
  context.lineTo(mouse.x, mouse.y);
  context.closePath();
  polygon.points.push(polygon.points[1]); 
  context.stroke();
  draw = false;  
  context.save();
  document.getElementById('btn-save').disabled=false;

}

function clearCanvas(){
  context.clearRect(0, 0, canvas.width, canvas.height);
  document.getElementById('draw').disabled=false;
  polygon.points=[];
  return false;
}

export default App;
