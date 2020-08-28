import React, { Component } from "react";
import { Grid, Typography } from "@material-ui/core";
import ProjectCard from "./ProjectCard";
export class Projects extends Component {
    render() {
        return (
            <div>
                <Grid item container>
                    <Grid xs={0} sm={2} />
                    <Grid item xs={12} sm={8}>
                        <Typography variant="h6" style={{ marginTop: "50px" }}>
                            Advisors
                        </Typography>
                        <Grid container spacing={4}>
                            <ProjectCard />
                        </Grid>
                    </Grid>
                </Grid>
                <Grid xs={0} sm={2} />
            </div>
        );
    }
}

export default Projects;
