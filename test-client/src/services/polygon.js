import axios from 'axios'

export const fetchPolygons = async (request) => {
    try{
        var response = await axios.get('http://localhost:5000/api/polygons');
        return response.data.polygons;
    } catch(e) {
        console.error(e);
    }

}

export const savePolygon = async (request) => {
    try {
        var response = await axios.post('http://localhost:5000/api/polygons',request,
        {
            headers: {
              'Content-Type': 'application/json'
            }
    });
    setPo

        return response.data;
    }catch(e) {
        console.error(e);
    }
};

export const checkPoint = async (request) => {
    try {
        var response = await axios.post('http://localhost:5000/api/pointInPolygons',request,
        {
            headers: {
              'Content-Type': 'application/json'
            }
        });
        if(response.data == true) {
            alert("Точка принадлежит многоугольнику");
        }
        else {
            alert("Точка находится за пределами многоугольника");
        }
        return console.log(response.data);
    }catch(e) {
        console.error(e);
    }
};