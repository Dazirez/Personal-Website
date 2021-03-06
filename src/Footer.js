import React from "react";
import styled from "styled-components";
const Wrapper = styled.section`
    display: flex;
    margin-top: 5rem;
    justify-content: space-around;
    padding: 1rem;
    background-color: black;
    bottom: 0;
    left: 0;
    width: 100%;
    max-height: 100%;
`;
const Text = styled.p`
    color: white;
    font-size: 10px;
`;
const Footer = () => (
    <Wrapper>
        <Text>Info</Text>
        <Text>About</Text>
        <Text>Contact</Text>
        <Text>FAQ</Text>
        <Text>Terms and Conditions | Private Policy</Text>
    </Wrapper>
);

export default Footer;
