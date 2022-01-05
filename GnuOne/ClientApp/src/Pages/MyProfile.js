import React from 'react';
import { Link, Switch } from 'react-router-dom';
import friends from '../icons/friends.svg'
import trash from '../icons/trash.svg'
import './MyProfile.min.css'
import RouteWithSubRoutes from '../components/NewComponents/RouteWithSubRoutes';


//testar
function MyProfile({ routes }) {
    const menu = [
        {
            path: '/profile/friends', // the url
            img: friends, // the img that appears in the wheel
        },
        {
            path: '/profile/bio',
            img: trash,
        },
        {
            path: '/profile/messages',
            img: trash,
        },
        {
            path: '/profile/settings',
            img: trash,
        },
        {
            path: '/profile/discussions',
            img: trash,
        },
        {
            path: '/profile/testwheel',
            img: trash,
        },
    ];


    return (
        <> <section className="profile-wheel-container">
            <div className="profile-wheel-wrapper">
                <ul className='profile-wheel'>
                    {menu.map((menuItem) => (
                       
                            <li key={menuItem.name}>
                                <Link to={menuItem.path}><img alt="icon person" src={menuItem.img} /></Link>
                            </li>
                          
                       
                    ))}
                    <li>
                        <img alt="icon person" src="./profile-icon.svg" />
                    </li>
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

export default MyProfile;
