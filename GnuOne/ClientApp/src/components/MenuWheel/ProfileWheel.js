import React, { useState, useContext } from 'react';
import { Link, Switch, useRouteMatch } from 'react-router-dom';
import friends from '../../icons/friends.svg';
import messages from '../../icons/messages.svg';
import file from '../../icons/file.svg';
import settings from '../../icons/settings-normal.svg';
import bio from '../../icons/bio.svg';
import saved from '../../icons/saved.svg';
import './profileWheel.min.css';
import RouteWithSubRoutes from '../RouteWithSubRoutes';
import WheelContext from '../../contexts/WheelContext'
import ProfilePicContext from '../../contexts/profilePicContext'
import MeContext from '../../contexts/meContext'
import FriendContext from '../../contexts/friendContext'
import images from '../../Image';

//testar
function ProfileWheel({ routes }) {
    const myEmail = useContext(MeContext);
    const { friendEmail, friendImg } = useContext(FriendContext)
    const { chosenPage, setChosenPage, active, setActive, done, setDone } = useContext(WheelContext);
    const { profilePic } = useContext(ProfilePicContext);
    console.log(profilePic)
    const avatar = friendEmail === undefined ? images[`Img${profilePic}`] : images[`Img${friendImg}`]
    console.log(friendEmail)
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
            img: bio,
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
            path: `${match.url}/saved`,
            img: saved,
            id: 6
        },
    ];

    const menuFriend = [
        {
            path: `${match.url}/friends`, // the url
            img: friends, // the img that appears in the wheel
            id: 1
        },
        {
            path: `${match.url}/bio`,
            img: bio,
            id: 2
        },
        {
            path: `${match.url}/messages`,
            img: messages,
            id: 3
        },
        {
            path: `${match.url}/discussions`,
            img: file,
            id: 5
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
            {friendEmail ===undefined ?

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
                        <img alt="img of person" src={avatar} />
                    </a>
                    {console.log(active, chosenPage)}
                    <div className="profile-wheel-lines-container">
                        <div className={active ? "line" : done ? "line-after" : "line"}></div>
                        <div className={active ? "line" : done ? "line-after" : "line"}></div>
                        <div className={active ? "line" : done ? "line-after" : "line"}></div>
                        <div className={active ? "line" : done ? "line-after" : "line"}></div>
                        <div className={active ? "line" : done ? "line-after" : "line"}></div>
                        <div className={active ? "line" : done ? "line-after" : "line"}></div>
                    </div>
                </ul>
                </div>
                :
                // friends wheel
                <div className="profile-wheel-wrapper">
                    <ul className='friends-wheel'>
                        {menuFriend.map((menuItem) => (
                            <Link key={menuItem.id} to={menuItem.path} onClick={() => handleClick(menuItem.id)} className={active && chosenPage === menuItem.id ? "chosen" : active ? "notChosen" : done && chosenPage === menuItem.id ? "initialPlace" : "notChosenAfter"} >
                                <li key={menuItem.name}>
                                    <img alt="icon person" src={menuItem.img} />
                                </li>
                            </Link>
                        ))}
                        <a className="img-of-person" >
                            <img alt="img of person" src={avatar} />
                        </a>
                        {console.log(active, chosenPage)}
                        <div className="profile-wheel-lines-container">
                            <div className={active ? "line" : done ? "line-after" : "line"}></div>
                            <div className={active ? "line" : done ? "line-after" : "line"}></div>
                            <div className={active ? "line" : done ? "line-after" : "line"}></div>
                            <div className={active ? "line" : done ? "line-after" : "line"}></div>
                        </div>
                    </ul>
                </div>
            }
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