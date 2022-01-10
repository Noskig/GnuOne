import React, { useState } from 'react';
import { Link, Switch } from 'react-router-dom';
import friends from '../icons/friends.svg'
import ProfileWheel from '../components/NewComponents/ProfileWheel';


//testar
function MyProfile({ routes }) {


    return (
        <main className="main">

            <ProfileWheel routes={routes}/>
            
        </main>
    )
}

export default MyProfile;