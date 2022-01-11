import React from 'react';
import logo from '../../icons/trash.svg'
import avatar from '../../icons/trash.svg'

const NavBar = () => {

    return (
            <section>
            <div className="navbar">
                <h1> KROMPIR </h1>
                <img className="logo" src={logo} alt="logo"/>
                <img className="avatar" src={avatar} alt="avatar" />

                </div>
            </section>
        )
}

export default NavBar