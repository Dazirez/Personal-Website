import React, { Component } from "react";
import { Grid } from "@material-ui/core";
import ProjectCard from "./ProjectCard";
import styled from "styled-components";

const Proj = styled.div`
    margin: auto;
`;
export class Projects extends Component {
    render() {
        return (
            <Proj>
                <Grid container spacing={4} justify={"center"}>
                    <Grid xs={0} sm={3} />
                    <Grid item xs={12} sm={1}>
                        <h4 style={{ color: "#007bff" }}> Projects </h4>
                    </Grid>
                    <Grid item xs={12} sm={2}>
                        <Grid container spacing={4}>
                            <ProjectCard
                                title="Outlaws"
                                imgSrc={require("./assets/img/Outlaws.gif")}
                                link="https://github.com/Dazirez/yeehaw-frontend"
                            />
                        </Grid>
                    </Grid>
                    <Grid item xs={12} sm={2}>
                        <Grid container spacing={4}>
                            <ProjectCard
                                title="Offhours"
                                imgSrc={require("./assets/img/Offhours.png")}
                                link="https://github.com/Dazirez/offhours4"
                            />
                        </Grid>
                    </Grid>
                    <Grid item xs={12} sm={2}>
                        <Grid container spacing={4}>
                            <ProjectCard
                                title="Mentii"
                                imgSrc={require("./assets/img/Mentii.png")}
                                link="https://github.com/Dazirez/mentii-2"
                            />
                        </Grid>
                    </Grid>
                </Grid>
            </Proj>
        );
    }
}

export default Projects;
