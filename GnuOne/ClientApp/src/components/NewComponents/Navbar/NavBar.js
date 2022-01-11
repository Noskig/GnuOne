import React from 'react';
import logo from '../../../icons/Logo.svg'
import avatar from '../../../icons/useravatar.svg'
import "./navbar.css"

const NavBar = () => {

    return (
            <section>
                <div className="navbar">
                    <img className="logo" src={logo} alt="logo"/>
                    <img className="avatar" src={avatar} alt="avatar" />
                </div>
            </section>
        )
}

export default NavBar