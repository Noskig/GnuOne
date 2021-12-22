import React, { useRef } from 'react'
import "./Friends.css"
import { useDetectOutsideClick } from './useDetectOutsideClick';

const Dropdown = () => {
    const dropdownRef = useRef(null)
    const [isActive, setIsActive] = useDetectOutsideClick(dropdownRef);
    const onClick = () => setIsActive(!isActive)

    return (
        <>
            <div className="dropdownContainer" >
                    <div className="threeDots" onClick={onClick} ></div>
                    <nav ref={dropdownRef} className={`dropdown ${isActive ? "active" : "inactive"}`}  > 
                        <ul>
                            <li><a href= "#">Send DM</a> </li>
                            <li><a href= "#">Call</a></li>
                            <li><a href= "#">  Mute</a> </li>
                            <li><a style={{color:'red'}} href= "#"> Remove</a> </li>
                        </ul>
                    </nav>
            </div>
        </>
    )
}

export default Dropdown