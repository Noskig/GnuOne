﻿import React, { useState } from 'react';
import { Link, Switch } from 'react-router-dom';
import friends from '../../icons/friends.svg'
import trash from '../../icons/trash.svg'
import './profileWheel.min.css'
import RouteWithSubRoutes from './RouteWithSubRoutes';


//testar
function ProfileWheel({ routes }) {

    const [chosenPage, setChosenPage] = useState();
    const [active, setActive] = useState();
    const [done, setDone] = useState();

    const menu = [
        {
            path: '/profile/friends', // the url
            img: friends, // the img that appears in the wheel
            id: 1
        },
        {
            path: '/profile/bio',
            img: trash,
            id: 2
        },
        {
            path: '/profile/messages',
            img: trash,
            id: 3
        },
        {
            path: '/profile/settings',
            img: trash,
            id: 4
        },
        {
            path: '/profile/discussions',
            img: trash,
            id: 5
        },
        {
            path: '/profile/testwheel',
            img: trash,
            id: 6
        },
    ];

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
        <> <section className="profile-wheel-container">
            <div className="profile-wheel-wrapper">
                <ul className='profile-wheel'>
                    {menu.map((menuItem) => (
                        <Link to={menuItem.path} onClick={() => handleClick(menuItem.id)} className={active && chosenPage === menuItem.id ? "chosen" : active ? "notChosen" : done && chosenPage === menuItem.id ? "initialPlace" : "notChosenAfter"} >
                            <li key={menuItem.name}>
                                <img alt="icon person" src={menuItem.img} />
                            </li>
                        </Link>
                    ))}
                    <a className="img-of-person" >
                        <img alt="img of person" src={friends} />
                    </a>
                    {console.log(active, chosenPage)}
                    <div className="profile-wheel-lines-container">
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                        <div className="line"></div>
                    </div>
                </ul>
            </div>
        </section>
            <Switch>
                {routes.map((route, i) => (
                    <RouteWithSubRoutes key={i} {...route} />
                ))}
            </Switch>
        </>
    )
}

export default ProfileWheel;