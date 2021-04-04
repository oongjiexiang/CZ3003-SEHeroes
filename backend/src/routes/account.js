const express = require("express");
const router = express.Router();
const { register, login }  = require("../controllers/account-controller");
const {check, validationResult} = require("express-validator/check");

router.post('/register',
    [
        check('username', 'Username is required').not().isEmpty(),
        check('email', 'Please include a valid email').isEmail(),
        check('password', 'Password must have 6 or more characters').isLength({min: 6}),
        check(
            'password2',
            'confirm password should be same as password',
          )
            .exists()
            .custom((value, { req }) => value === req.body.password),
        check('matricNo', 'Matric number is required').not().isEmpty(),
        check('character', 'Character is requried').not().isEmpty()
    ],
    (req, res) => {

        const errors = validationResult(req);
        if(!errors.isEmpty()){
            return res.status(500).send({ message: `${errors.array()[0].msg}` });
        }

        const {username, email, password,  matricNo, character} = req.body;
        
        register({
            username, email, password, matricNo, character
        },
        (err, info) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send({message:"Register successfully", ...info});
            }
        })
    }
);


router.post('/login',
    [
        check('matricNo', 'Matric number is required').not().isEmpty(),
        check('password', 'Password is required').not().isEmpty()
    ],
    (req, res) => {

        const errors = validationResult(req);
        if(!errors.isEmpty()){
            return res.status(500).send({ message: `${errors.array()[0].msg}` });
        }

        const {password, matricNo} = req.body;
        
        login({
            password, matricNo
        },
        (err, info) => {
            if (err) {
                return res.status(500).send({ message: `${err}` });
            } else {
                return res.status(200).send({message:"Login successfully", ...info});
            }
        })
    }
);

module.exports = router