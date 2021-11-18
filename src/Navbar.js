import React, { Component } from 'react';
import styled from 'styled-components';

const Wrap = styled.div`
  display: flex;
  flex-direction: row;
  position: absolute;
  width: 100vw;
  justify-content: flex-end;
  align-content: space-around;
  gap: 20px;
  padding-top: 40px;
  padding-right: 50px;
`;

const NavItem = styled.a`
  text-decoration: none;
  font-size: 2em;
  font-weight: bold;
  color: #000;
  &:hover {
    color: #61dafb;
    text-decoration: none;
  }
`;

export class Navbar extends Component {
  render() {
    return (
      <Wrap>
        <NavItem href='/'>Home</NavItem>
        <NavItem href='/blog'>Blog</NavItem>
        <NavItem href='/'>About Me</NavItem>
      </Wrap>
    );
  }
}

export default Navbar;
