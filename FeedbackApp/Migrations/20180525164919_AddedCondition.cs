using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FeedbackApp.Migrations
{
    public partial class AddedCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedback_survey_SurveyId",
                table: "feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_question_feedback_FeedbackId",
                table: "question");

            migrationBuilder.DropIndex(
                name: "IX_question_FeedbackId",
                table: "question");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "question");

            migrationBuilder.AlterColumn<Guid>(
                name: "SurveyId",
                table: "feedback",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "condition",
                columns: table => new
                {
                    ConditionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<int>(nullable: false),
                    FeedbackId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_condition", x => x.ConditionId);
                    table.ForeignKey(
                        name: "FK_condition_feedback_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "feedback",
                        principalColumn: "FeedbackId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_condition_question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_condition_FeedbackId",
                table: "condition",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_condition_QuestionId",
                table: "condition",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_survey_SurveyId",
                table: "feedback",
                column: "SurveyId",
                principalTable: "survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedback_survey_SurveyId",
                table: "feedback");

            migrationBuilder.DropTable(
                name: "condition");

            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "question",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SurveyId",
                table: "feedback",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_question_FeedbackId",
                table: "question",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedback_survey_SurveyId",
                table: "feedback",
                column: "SurveyId",
                principalTable: "survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_question_feedback_FeedbackId",
                table: "question",
                column: "FeedbackId",
                principalTable: "feedback",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
