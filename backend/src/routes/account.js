const express = require("express");
const router = express.Router();
const { createAccount, getAllAccounts, getAccount, updateAccount, removeAccount }  = require("../controllers/account-controller");
const { isAuthenticated } = require("../utils/authenticated");
const { isAuthorized }  = require("../utils/authorized");

router.post('/signup',
    //isAuthenticated,
    //isAuthorized({ hasRole: ['admin', 'manager'] }),
    createAccount
);

router.get('/', [
    //isAuthenticated,
    //isAuthorized({ hasRole: ['admin', 'manager'] }),
    getAllAccounts
]);

// get :id user
router.get('/:id', [
    //isAuthenticated,
    //isAuthorized({ hasRole: ['admin', 'manager'], allowSameUser: true }),
    getAccount
]);

// updates :id user
router.patch('/:id', [
    //isAuthenticated,
    //isAuthorized({ hasRole: ['admin', 'manager'], allowSameUser: true }),
    updateAccount
]);

// deletes :id user
router.delete('/:id', [
    //isAuthenticated,
    //isAuthorized({ hasRole: ['admin', 'manager'] }),
    removeAccount
]);

module.exports = router