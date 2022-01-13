import React, { useState } from 'react';
import { Link, Switch, useRouteMatch } from 'react-router-dom';
import friends from '../../icons/friends.svg';
import trash from '../../icons/trash.svg';
import messages from '../../icons/messages.svg';
import file from '../../icons/file.svg';
import settings from '../../icons/settings-normal.svg';
import avatar from '../../icons/bio.svg'

import './profileWheel.min.css';
import RouteWithSubRoutes from '../RouteWithSubRoutes';



//testar
function ProfileWheel({ routes }) {

    const [chosenPage, setChosenPage] = useState();
    const [active, setActive] = useState();
    const [done, setDone] = useState();
    let match = useRouteMatch()
    console.log(match)
    const menu = [
        {
            path: `${match.url}/friends`, // the url
            img: friends, // the img that appears in the wheel
            id: 1
        },
        {
            path: `${match.url}/bio`,
            img: avatar,
            id: 2
        },
        {
            path: `${match.url}/messages`,
            img: messages,
            id: 3
        },
        {
            path: `${match.url}/settings`,
            img: settings,
            id: 4
        },
        {
            path: `${match.url}/discussions`,
            img: file,
            id: 5
        },
        {
            path: `${match.url}/testwheel`,
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
                        <Link key={menuItem.id} to={menuItem.path} onClick={() => handleClick(menuItem.id)} className={active && chosenPage === menuItem.id ? "chosen" : active ? "notChosen" : done && chosenPage === menuItem.id ? "initialPlace" : "notChosenAfter"} >
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