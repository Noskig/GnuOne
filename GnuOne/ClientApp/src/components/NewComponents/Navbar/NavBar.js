import React from 'react';
import logo from '../../../icons/Logo.svg'
import avatar from '../../../icons/useravatar.svg'
import settings from '../../../icons/settings.svg'
import messages from '../../../icons/messagesNavbar.svg'

import "./navbar.css"

const NavBar = () => {

    return (
            <section>
                <div className="navbar">
                     <img src={logo} alt="logo" />
                     <div className="menu" >
                     <ul className="elements">
                        <li>
                            <img className="avatar" src={avatar} alt="avatar" />
                        </li>
                        <li>
                            <img className="messages" src={messages} alt="messages" />
                        </li>
                        <li>
                            <img className="settings" src={settings} alt="settings" />
                        </li>
                        </ul>
                    </div>
                </div>
            </section>
        )
}

export default NavBar