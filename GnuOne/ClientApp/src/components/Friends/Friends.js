import "./Friends.css"
import Dropdown from './Dropdown'

const Friends = () => {


    return (
        <>
             <div className="friendsDiv">
              <div>Kompis 1</div>
              <Dropdown/>
            </div>
        </>
    )
}

export default Friends

/* 
<div className="dropdownContainer" tabindex="-1">
                  <div className="threeDots"></div>
                  <div className="dropdown">
                      <a href= "#"> <div> Send direct message</div></a> 
                      <a href= "#"> <div> Call</div></a> 
                      <a href= "#"> <div> Mute</div></a> 
                      <a href= "#"> <div>Remove</div></a> 
                  </div>
              </div>
*/