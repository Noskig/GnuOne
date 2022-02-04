import React, { useState, useContext} from 'react';
import { Link } from 'react-router-dom'
/*import logo from '../../icons/Logo.svg'*/
import logo from '../../Image/Logo/gnux.png'
import settings from '../../icons/settings.svg'
import messages from '../../icons/messagesNavbar.svg'
import notificationsIcon from '../../icons/Group 220.svg'
import ProfilePicContext from '../../contexts/profilePicContext'
import WheelContext from '../../contexts/WheelContext'
import "./navbar.css"
import images from '../../Image';


const NavBar = () => {
    const [isPressed, setIsPressed] = useState(false);
    const { setChosenPage, setActive, setDone } = useContext(WheelContext);
    const { profilePic } = useContext(ProfilePicContext);
    console.log(profilePic)
    const avatar = images[`Img${profilePic}`]
 
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
            <Link className="logo" to="/" onClick={() => handleClick(0)}> <img src={logo} alt="logo" /> </Link>
            <ul className="elements">
                <li onClick={() => setIsPressed(!isPressed)} className="avatar">
                    {profilePic && avatar? <img src={avatar} alt="avatar" /> :null} 
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
                        <img className="profile-picture" src={notificationsIcon} alt="profile" />
                    </Link>
                </li>
                    
                </ul>
            </div>
        )
}

export default NavBar