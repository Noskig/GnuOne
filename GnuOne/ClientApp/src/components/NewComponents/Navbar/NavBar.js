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
                <ul id="menu">
                    <a class="menu-button icon-plus" href="#menu" title="Show navigation"></a>
                    <a class="menu-button icon-minus" href="#0" title="Hide navigation"></a>
                    <li class="menu-item">
                        <a href="#menu">
                            <span class="fa fa-github"></span>
                        </a>
                    </li>
                    <li class="menu-item">
                        <a href="#menu">
                            <span class="fa fa-linkedin"></span>
                        </a>
                    </li>
                    <li class="menu-item">
                        <a href="#menu">
                            <span class="fa fa-instagram"></span>
                        </a>
                    </li>
                    <li class="menu-item">
                        <a href="#menu">
                            <span class="fa fa-twitter"></span>
                        </a>
                    </li>
                </ul>
                </div>
            </section>
        )
}

export default NavBar