import React, { useState, useContext} from 'react';
import { Link, Switch, useRouteMatch } from 'react-router-dom'
import logo from '../../icons/Logo.svg'
import avatar from '../../icons/useravatar.svg'
import settings from '../../icons/settings.svg'
import messages from '../../icons/messagesNavbar.svg'
import "./navbar.css"
import WheelContext from '../../contexts/WheelContext'


const NavBar = () => {

    const [isPressed, setIsPressed] = useState(false);
   
    const { setChosenPage, setActive,  setDone } = useContext(WheelContext);

    function handleClick(id) {

        setChosenPage(id);
        setActive(true);
        setTimeout(animationEnd, 500)
    }

    function animationEnd() {

        setActive(false)
        setDone(true)
    }
    return (
          
            <div className="navbar">
                <img src={logo} alt="logo" />

            <ul className="elements">
               <li onClick={()=> setIsPressed(!isPressed)} className="avatar">
                  <img src={avatar} alt="avatar" />
               </li>
                    
                <li className={isPressed ? "messages out-animation1" :"messages in-animation1"}>
                    <Link to="/profile/messages" onClick={() => handleClick(3)}>
                        <img src={messages} alt="messages" />
                    </Link>
                </li>

                <li className={isPressed ? "settings out-animation2" :"settings in-animation2"}>
                    <Link to="/profile/settings"  onClick={() => handleClick(4)}>
                        <img src={settings} alt="settings" />
                    </Link>
               </li>
                    
                </ul>
            </div>
        )
}

export default NavBar