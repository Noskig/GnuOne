import React, { useState, useContext} from 'react';
import { Link, Switch, useRouteMatch } from 'react-router-dom'
import logo from '../../icons/Logo.svg'
import avatar from '../../icons/useravatar.svg'
import settings from '../../icons/settings.svg'
import messages from '../../icons/messagesNavbar.svg'
import profile from '../../icons/Gnu 220.png'
import ProfilePicContext from '../../contexts/profilePicContext'
import WheelContext from '../../contexts/WheelContext'
import "./navbar.css"
import images from '../../Image';


const NavBar = () => {
    const [isPressed, setIsPressed] = useState(false);
    const { setChosenPage, setActive,  setDone } = useContext(WheelContext);
    const picID = useContext(ProfilePicContext);
    console.log(picID)
    const avatar = images[`Img${picID}`]
 
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
            <Link to="/" onClick={() => handleClick(0)}> <img src={logo} alt="logo" /> </Link>

            <ul className="elements">
                <li onClick={() => setIsPressed(!isPressed)} className="avatar">
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

                <li className={isPressed ? "profile out-animation3" : "profile in-animation3"}>
                    <Link to="/profile" onClick={() => handleClick(0)}>
                        <img className="profile-picture" src={profile} alt="profile" />
                    </Link>
                </li>
                    
                </ul>
            </div>
        )
}

export default NavBar