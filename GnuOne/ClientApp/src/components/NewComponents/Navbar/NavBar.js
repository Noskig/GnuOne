import React from 'react';
import { useState } from 'react';
import logo from '../../../icons/Logo.svg'
import avatar from '../../../icons/useravatar.svg'
import settings from '../../../icons/settings.svg'
import messages from '../../../icons/messagesNavbar.svg'
import "./navbar.css"



const NavBar = () => {

    const [isPressed, setIsPressed ] = useState(false);

    return (
          
            <div className="navbar">
                <img src={logo} alt="logo" />

            <ul className="elements">
                <li onClick={()=> setIsPressed(!isPressed)} className="avatar">
                        <img src={avatar} alt="avatar" />
                    </li>
                <li className={isPressed ? "messages animation1" :"messages"}>
                        <img src={messages} alt="messages" />
                    </li>
                <li className={isPressed ? "settings animation2" :"settings"}>
                        <img src={settings} alt="settings" />
                    </li>
                </ul>
            </div>
        )
}

export default NavBar