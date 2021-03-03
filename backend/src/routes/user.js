const express = require("express");
const router = express.Router();
const { createUser, getAllUsers, getUser, updateUser, removeUser }  = require("../controllers/user-controller");
const { isAuthenticated } = require("../auth/authenticated.js");
const { isAuthorized }  = require("../auth/authorized");

router.post('/',
    isAuthenticated,
    isAuthorized({ hasRole: ['admin', 'manager'] }),
    createUser
);

router.get('/', [
    //isAuthenticated,
    //isAuthorized({ hasRole: ['admin', 'manager'] }),
    getAllUsers
]);

// get :id user
router.get('/:id', [
    isAuthenticated,
    isAuthorized({ hasRole: ['admin', 'manager'], allowSameUser: true }),
    getUser
]);

// updates :id user
router.patch('/:id', [
    isAuthenticated,
    isAuthorized({ hasRole: ['admin', 'manager'], allowSameUser: true }),
    updateUser
]);

// deletes :id user
router.delete('/:id', [
    isAuthenticated,
    isAuthorized({ hasRole: ['admin', 'manager'] }),
    removeUser
]);

module.exports = router