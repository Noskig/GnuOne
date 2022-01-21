const express = require('express')
const app = express()
const http = require('http')
const cors = require('cors')

app.use(cors());

const server = http.createServer(app)

server.listen(7261, () => {
    console.log("server is running")
});