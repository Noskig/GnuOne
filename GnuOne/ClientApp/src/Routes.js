import MyProfile from './Pages/MyProfile';
import FriendProfile from './Pages/FriendProfile';
import Friends from './components/Friends/Friends';
import Bio from './components/Bio/Bio';
import Messages from './components/Messages/Messages';
import PrivateMessages from './components/Messages/PrivateMessages';
import Settings from './components/Settings/Settings';
import Discussions from './components/Discussions/Discussions';
import Posts from './components/Posts';
import Saved from './components/Saved/Saved';
import Comments from './components/Comments';
import Notifications from './components/Notifications/Notification';

const routes = [
    {
        path: '/friendprofile/:email',
        component: FriendProfile,
        routes: [
            {
                // Also note how we added /home before the 
                // actual page name just to create a complete path
                path: '/friendprofile/:email/friends',
                component: Friends,
            },
            {
                path: '/friendprofile/:email/bio',
                component: Bio,
            },
            {
                path: '/friendprofile/:email/messages',
                component: Messages,
            },
            {
                path: '/friendprofile/:email/settings',
                component: Settings,
            },
            {
                path: '/friendprofile/:email/discussions/:id/post/:id',
                component: Comments,
            },
            {
                path: '/friendprofile/:email/discussions/:id',
                component: Posts,
            },
            {
                path: '/friendprofile/:email/discussions',
                component: Discussions,

            },
        ],
    },
    {
        path: '/profile',
        component: MyProfile,
        routes: [
            {
                // Also note how we added /home before the 
                // actual page name just to create a complete path
                path: '/profile/friends',
                component: Friends,
            },
            {
                path: '/profile/bio',
                component: Bio,
            },
            {
                path: '/profile/messages/:email',
                component: PrivateMessages,
            },
            {
                path: '/profile/messages',
                component: Messages,
            },
            {
                path: '/profile/settings',
                component: Settings,
            },
            {
                path: '/profile/discussions/:id/post/:id',
                component: Comments,
            },
            {
                path: '/profile/discussions/:id',
                component: Posts,
            },
            {
                path: '/profile/discussions',
                component: Discussions,

            },
            {
                path: '/profile/saved',
                component: Saved,

            },
            {
                path: '/',
                component: Notifications,
            },
        ],
    },
];

export default routes;